using System.Windows.Forms;

namespace dcPrevent.GMA {

    public interface IKeyboardEvents {

        event KeyEventHandler KeyDown;

        event KeyPressEventHandler KeyPress;

        event KeyEventHandler KeyUp;

    }

}