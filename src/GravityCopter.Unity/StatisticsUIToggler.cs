using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Inputs;

namespace GravityCopter.Unity {

    public class StatisticsUIToggler : Updatable {

        public StartStopInput ToggleStatsInput;
        public StatisticsUI StatisticsUI;

        protected override void BetterAwake() {
            base.BetterAwake();

            Assert.IsNotNull(ToggleStatsInput, this.GetAssociationAssertion(nameof(ToggleStatsInput)));
            Assert.IsNotNull(StatisticsUI, this.GetAssociationAssertion(nameof(StatisticsUI)));

            RegisterUpdatesAutomatically = true;
            BetterUpdate = doUpdate;
        }

        private void doUpdate(float deltaTime) {
            GameObject rootObj = StatisticsUI.Root.gameObject;
            if (ToggleStatsInput.Started())
                rootObj.SetActive(!rootObj.activeSelf);
        }
    }

}
