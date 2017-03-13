﻿using Zenject;
using UniRx;
using UnityEngine;

namespace LlapiExample
{
    public class RepositoriesForConnectionLogic : INetworkLogic
    {
        [Inject]
        private ConnectionsRepository connections;

        [Inject]
        private DiContainer container;

        public void Setup()
        {
            connections.Connections.Subscribe(OnConnect);
            connections.Disconnections.Subscribe(OnDisconnect);
        }

        private void OnConnect(int connectionId)
        {
            var instance = container.Instantiate<CharacterStatus>();
            container.Bind<CharacterStatus>().WithId(connectionId).FromInstance(instance).AsCached();
        }

        private void OnDisconnect(int connectionId)
        {
            container.UnbindId<CharacterStatus>(connectionId);
        }
        
        public void Update()
        {
        }
    }
}
