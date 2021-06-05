using System;
using System.Data.Common;
using Assets.Scripts.Plants;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Farmer.Action
{
    public abstract class ActionBase
    {
        private Guid _id;

        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty)
                    _id = Guid.NewGuid();
                return _id;
            }
        }

        protected Vector3Int GridPosition { get; }
        protected abstract string Name { get; }
        protected Seed Seed { get; }

        public string GetName()
        {
            return this.Name;
        }
        public Vector3Int GetPosition()
        {
            return this.GridPosition;
        }
        public ActionBase(Vector3Int gridLocation, Seed seed)
        {
            this.GridPosition = gridLocation;
            this.Seed = seed;
        }

        public ActionBase(Vector3Int gridLocation)
        {
            this.GridPosition = gridLocation;
        }

        public ActionBase()
        {
            
        }
    }
}