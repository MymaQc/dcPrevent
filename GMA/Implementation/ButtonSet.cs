using System.Windows.Forms;

namespace dcPrevent.GMA.Implementation {

    internal class ButtonSet {

        private MouseButtons mouseButtonsSet;

        public ButtonSet() => mouseButtonsSet = MouseButtons.None;

        public void Add(MouseButtons element) => mouseButtonsSet |= element;

        public void Remove(MouseButtons element) => mouseButtonsSet &= ~element;

        public bool Contains(MouseButtons element) => (mouseButtonsSet & element) != 0;

    }

}