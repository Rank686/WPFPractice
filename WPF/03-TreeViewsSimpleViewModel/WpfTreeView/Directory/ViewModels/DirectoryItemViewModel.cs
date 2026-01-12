using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfTreeView
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType pub_Type { get; set; }
        public string pub_ImageName => pub_Type == DirectoryItemType.Drive ? "drive" : pub_Type == DirectoryItemType.File ? "file" : pub_IsExpanded ? "folder-open" : "folder-closed";
        /// <summary>
        /// The full path to the item
        /// </summary>
        public string pub_FullPath { get; set; }
        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string pub_Name => pub_Type == DirectoryItemType.Drive ? pub_FullPath : DirectoryStructure.GetFileFolderName(pub_FullPath);
        /// <summary>
        /// A list of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> pub_Children { get; set; }
        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool pub_CanExpand => pub_Type != DirectoryItemType.File;
        /// <summary>
        /// Indicates if the current item is expanded or not
        /// </summary>
        public bool pub_IsExpanded
        {
            get => pub_Children?.Count(f => f != null) > 0;
            set
            {
                // If the UI tells us to expand...
                if (value == true)
                    // Find all children
                    Expand();
                // If the UI tells us to close
                else
                    ClearChildren();
            }
        }
        #endregion

        #region Public Commands
        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="pubFullPath">The full path of this item</param>
        /// <param name="pubType">The type of item</param>
        public DirectoryItemViewModel(string pubFullPath, DirectoryItemType pubType)
        {
            // Create commands
            ExpandCommand = new RelayCommand(Expand);

            // Set path and type
            pub_FullPath = pubFullPath;
            pub_Type = pubType;

            // Setup the children as needed
            ClearChildren();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Removes all children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            // Clear items
            pub_Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show the expand arrow if we are not a file
            if (pub_Type != DirectoryItemType.File)
                pub_Children.Add(null);
        }

        #endregion

        /// <summary>
        ///  Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            // We cannot expand a file
            if (pub_Type == DirectoryItemType.File)
                return;

            // Find all children
            var children = DirectoryStructure.GetDirectoryContents(pub_FullPath);
            pub_Children = new ObservableCollection<DirectoryItemViewModel>(
                                children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
