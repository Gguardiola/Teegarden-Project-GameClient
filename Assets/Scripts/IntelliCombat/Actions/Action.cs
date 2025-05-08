using System;
using UnityEngine;

namespace IntelliCombat.Actions
{
    public abstract class Action : MonoBehaviour
    {
        [HideInInspector]
        public bool isActionInProgress;
        public abstract void PerformAction();

        public virtual void CancelAction()
        {
            //Do nothing. Override this method if custom behavior is needed.

        }
        public abstract bool EndTurnAction();

        public virtual bool ResolveAction(TurnResolver turnResolver)
        {
            return turnResolver.ResolveTurn(this);
        }

        public virtual void FirstReactorEvent<T>(T eventData)
        {
            //Do nothing. Override this method if custom behavior is needed.
        }
    }
}