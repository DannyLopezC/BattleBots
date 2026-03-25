using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleBots.BuildMode
{
    public class BuildInputActions
    {
        public InputAction click;
        public InputAction remove;
        public InputAction cancel;

        public BuildInputActions(InputAction click, InputAction remove, InputAction cancel)
        {
            this.click = click;
            this.remove = remove;
            this.cancel = cancel;
        }
    }
}
