```mermaid
sequenceDiagram
participant App as WPF App
participant MainWindow as MainWindow
participant DirVM as DirectoryStructureViewModel
participant DirStruct as DirectoryStructure
participant ItemVM as DirectoryItemViewModel
participant UI as TreeView

    App->>MainWindow: StartupUri loads MainWindow.xaml
    MainWindow->>MainWindow: InitializeComponent()
    MainWindow->>DirVM: new DirectoryStructureViewModel()
    DirVM->>DirStruct: GetLogicalDrives()
    DirStruct-->>DirVM: List<DirectoryItem>
    DirVM->>DirVM: Create Items (DirectoryItemViewModel list)
    MainWindow-->>UI: DataContext = DirVM
    UI-->>UI: Bind ItemsSource to Items

    UI->>ItemVM: User expands node (IsExpanded=true)
    ItemVM->>ItemVM: Expand()
    ItemVM->>DirStruct: GetDirectoryContents(fullPath)
    DirStruct-->>ItemVM: List<DirectoryItem>
    ItemVM->>ItemVM: Create Children (DirectoryItemViewModel list)
    ItemVM-->>UI: Children updated -> TreeView renders
