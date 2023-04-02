using System;
using System.Collections.Generic;
using Infrastructure.Factories;
using Zenject;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine, IInitializable
    {
        private readonly IStatesFactory _statesFactory;
        private IExitableState _activeState;
        private Dictionary<Type, IExitableState> _states;

        public GameStateMachine(IStatesFactory statesFactory)
        {
            _statesFactory = statesFactory;
        }

        public void Initialize()
        {
            if (_states != null)
            {
                return;
            }

            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = _statesFactory.Create<BootstrapState>(),
                [typeof(LoadAssetsState)] = _statesFactory.Create<LoadAssetsState>(),
                [typeof(LoadProgressState)] = _statesFactory.Create<LoadProgressState>(),
                [typeof(LoadLevelState)] = _statesFactory.Create<LoadLevelState>(),
                [typeof(GameLoopState)] = _statesFactory.Create<GameLoopState>(),
                [typeof(RestartState)] = _statesFactory.Create<RestartState>(),
                [typeof(ExitGameState)] = _statesFactory.Create<ExitGameState>()
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}