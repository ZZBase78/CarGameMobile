using System;
using JetBrains.Annotations;
using Features.Inventory.Items;
using UnityEngine;

namespace Features.Inventory
{
    internal interface IInventoryController
    {
    }

    internal class InventoryController : BaseController, IInventoryController
    {
        private readonly InventoryView _view;
        private readonly InventoryModel _model;
        private readonly ItemsRepository _repository;


        public InventoryController(Transform placeForUi, InventoryModel inventoryModel)
        {
            _view = new InventoryFactory().CreateView(placeForUi);
            AddGameObject(_view.gameObject);

            _model = inventoryModel;

            _repository = new ItemsRepositoryFactory().Create();
            AddRepository(_repository);

            _view.Display(_repository.Items.Values, OnItemClicked);

            foreach (string itemId in _model.EquippedItems)
                _view.Select(itemId);
        }

        protected override void OnDispose()
        {
            _view.Clear();
            base.OnDispose();
        }


        private void OnItemClicked(string itemId)
        {
            bool equipped = _model.IsEquipped(itemId);

            if (equipped)
                UnequipItem(itemId);
            else
                EquipItem(itemId);
        }

        private void EquipItem(string itemId)
        {
            _view.Select(itemId);
            _model.EquipItem(itemId);
        }

        private void UnequipItem(string itemId)
        {
            _view.Unselect(itemId);
            _model.UnequipItem(itemId);
        }
    }
}
