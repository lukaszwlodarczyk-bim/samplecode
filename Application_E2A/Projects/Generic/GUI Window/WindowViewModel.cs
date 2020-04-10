using System.Windows;
using System.Windows.Input;

namespace Application_E2A.Projects
{
    public class WindowViewModel : BaseViewModel
    {
        #region private Fields
        /// The window this view model controls
        private Window mWindow;
        /// The margin around the window to allow for a drop shadow
        private int mOuterMarginSize = 10;
        /// The radius of the edges of the window
        private int mWindowRadius = 10;
        /// The last known dock position
        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;
        #endregion

        #region public Properties
        /// The smallest width the window can go to
        public double WindowMinimumWidth { get; set; } = 450;
        /// The smallest height the window can go to
        public double WindowMinimumHeight { get; set; } = 450;
        /// True if the window should be borderless because it is docked or maximized
        public bool Borderless { get { return (mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked); } }
        /// The size of the resize border around the window
        public int ResizeBorder { get; set; } = 6;
        /// The size of the resize border around the window, taking into account the outer margin
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }
        /// The padding of the inner content of the main window
        public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder); } }
        /// The margin around the window to allow for a drop shadow
        public int OuterMarginSize
        {
            get
            {
                // If it is maximized or docked, no border
                return Borderless ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }
        /// The margin around the window to allow for a drop shadow
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }
        /// The radius of the edges of the window
        public int WindowRadius
        {
            get
            {
                // If it is maximized or docked, no border
                return Borderless ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }
        /// The radius of the edges of the window
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }
        /// The height of the title bar / caption of the window
        public int TitleHeight { get; set; } = 42;
        /// The height of the title bar / caption of the window
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        public string CurrentDocument { get; set; } = ThisApplication.thisApp.doc.Title;

        public System.Windows.Visibility RefreshButtonVisible { get; set; } = System.Windows.Visibility.Visible;
        #endregion

        #region Commands
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        #endregion

        #region Constructor
        public WindowViewModel(Window window, bool isModal = false)
        {
            mWindow = window;

            // Listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                WindowResized();
            };

            // Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            if (isModal == false)
                CloseCommand = new RelayCommand(() => mWindow.Visibility = System.Windows.Visibility.Hidden);
            else
                CloseCommand = new RelayCommand(() => mWindow.Close() );

            // Fix window resize issue
            var resizer = new WindowResizer(mWindow);
            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                mDockPosition = dock;
                // Fire off resize events
                WindowResized();
            };
        }
        #endregion

        #region Private Helpers
        /// Gets the current mouse position on the screen
        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        /// If the window resizes to a special position (docked or maximized)
        /// this will update all required property change events to set the borders and radius values
        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }
        #endregion
    }
}
