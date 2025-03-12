using BLL.DTO;
using BLL.Interface;
using DAL.IUnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class DataService : IDataService
    {
        private IUnitOfWork _unitOfWork;
        public DataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DataLoadDTO> LoadData(string username)
        {
            DataLoadDTO dataLoadDTO = new DataLoadDTO();
            var accountList = await _unitOfWork.AccountRepository.Include(x => x.Checkpoints, x => x.InventoryItems, x => x.Equipment).GetAllAsync();
            var account = accountList.FirstOrDefault(x => x.Username == username);
            dataLoadDTO.ScreenName = account.ScreenName;
            dataLoadDTO.Gold = account.Gold;
            dataLoadDTO.Strength = account.Strength;
            dataLoadDTO.Agility = account.Agility;
            dataLoadDTO.Intelligence = account.Intelligence;
            dataLoadDTO.Vitality = account.Vitality;

            dataLoadDTO.Inventory = new Dictionary<string, int>();
            if (account.InventoryItems != null)
            {
                foreach (var item in account.InventoryItems)
                {
                    dataLoadDTO.Inventory.Add(item.ItemDataId, int.Parse(item.Value));
                }
            }

            dataLoadDTO.EquipmentId = new List<string>();
            if (account.Equipment != null)
            {
                foreach (var item in account.Equipment)
                {
                    dataLoadDTO.EquipmentId.Add(item.EquipmentItemId);
                }
            }

            var checkpoint = await _unitOfWork.CheckpointRepository.FindAsync(x => x.AccountId == account.Id && x.IsFinal);
            dataLoadDTO.CloseCheckpointId = checkpoint?.CheckpointId;

            dataLoadDTO.CheckPoints = new Dictionary<string, bool>();
            if (checkpoint != null)
            {
                var checkpointList = await _unitOfWork.CheckpointRepository.FindAllAsync(x => x.AccountId == account.Id);
                foreach (var item in checkpointList)
                {
                    dataLoadDTO.CheckPoints.Add(item.CheckpointId, item.Status);
                }
            }

            return dataLoadDTO;
        }


        public async Task SaveData(DataSaveDTO dataSaveDTO, string username)
        {
            var accountList = await _unitOfWork.AccountRepository.Include(x => x.Checkpoints, x => x.InventoryItems, x => x.Equipment).GetAllAsync();
            var account = accountList.FirstOrDefault(x => x.Username == username);
            account.ScreenName = dataSaveDTO.ScreenName;
            account.Gold = dataSaveDTO.Gold;
            account.Strength = dataSaveDTO.Strength;
            account.Agility = dataSaveDTO.Agility;
            account.Intelligence = dataSaveDTO.Intelligence;
            account.Vitality = dataSaveDTO.Vitality;

            foreach (var item in dataSaveDTO.Inventory.Keys.Zip(dataSaveDTO.Inventory.Values, (key, value) => new { key, value }))
            {
                var inventoryItem = account.InventoryItems?.FirstOrDefault(x => x.ItemDataId == item.key);
                if (inventoryItem != null)
                {
                    inventoryItem.Value = item.value.ToString();
                }
                else
                {
                    await _unitOfWork.InventoryItemRepository.AddAsync(new DAL.Entities.InventoryItem
                    {
                        AccountId = account.Id,
                        ItemDataId = item.key,
                        Value = item.value.ToString()
                    });
                }
            }
            if (account.InventoryItems != null)
            {
                foreach (var item in account.InventoryItems)
                {
                    if (!dataSaveDTO.Inventory.Keys.Contains(item.ItemDataId))
                    {
                        await _unitOfWork.InventoryItemRepository.RemoveAsync(item);
                    }
                }
            }
           

            foreach (var item in dataSaveDTO.EquipmentId)
            {
                var equipment = account.Equipment?.FirstOrDefault(x => x.EquipmentItemId == item);
                if (equipment == null)
                {
                    await _unitOfWork.EquipmentRepository.AddAsync(new DAL.Entities.Equipment
                    {
                        AccountId = account.Id,
                        EquipmentItemId = item
                    });
                }
            }
            if(account.Equipment != null)
            {
                foreach (var item in account.Equipment)
                {
                    if (!dataSaveDTO.EquipmentId.Contains(item.EquipmentItemId))
                    {
                        await _unitOfWork.EquipmentRepository.RemoveAsync(item);
                    }
                }
            } 
            

            foreach (var item in dataSaveDTO.CheckPoints.Keys.Zip(dataSaveDTO.CheckPoints.Values, (key, value) => new { key, value }))
            {
                var checkpoint = account.Checkpoints.FirstOrDefault(x => x.CheckpointId == item.key);
                if (checkpoint != null)
                {
                    checkpoint.Status = item.value;
                }
                else
                {
                   await _unitOfWork.CheckpointRepository.AddAsync(new DAL.Entities.Checkpoint
                    {
                        AccountId = account.Id,
                        CheckpointId = item.key,
                        Status = item.value
                    });
                }
            }

            if(account.Checkpoints != null)
            {
                foreach (var item in account.Checkpoints)
                {
                    if (!dataSaveDTO.CheckPoints.Keys.Contains(item.CheckpointId))
                    {
                        await _unitOfWork.CheckpointRepository.RemoveAsync(item);
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
