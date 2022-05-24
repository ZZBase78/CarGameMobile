using System;
using System.Collections.Generic;
using Features.Shed.Upgrade;
using Game.Transport;

namespace Features.Shed
{
    internal class ShedController : BaseController
    {
        public event Action OnDone = delegate { };
        public event Action OnBack = delegate { };

        private readonly ShedView _view;
        private TransportModel _transportModel;


        public ShedController(TransportModel transportModel, ShedView shedView)
        {
            _transportModel = transportModel;
            _view = shedView;

            _view.Init(Apply, Back);
        }

        private void Apply()
        {
            OnDone.Invoke();
            Log($"Apply. Current Speed: {_transportModel.Speed}");
        }

        private void Back()
        {
            OnBack.Invoke();
            Log($"Back. Current Speed: {_transportModel.Speed}");
        }


        private void ClearAllListeners()
        {
            OnDone = delegate { };
            OnBack = delegate { };
        }

        protected override void OnDispose()
        {
            ClearAllListeners();
            _view.Deinit();
        }

    }
}
