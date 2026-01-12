# WpfTreeView 类图

```mermaid
classDiagram
class MainWindow {
    +MainWindow()
}

class DirectoryStructureViewModel {
    +ObservableCollection~DirectoryItemViewModel~ Items
    +DirectoryStructureViewModel()
}

class DirectoryItemViewModel {
    +DirectoryItemType Type
    +string FullPath
    +string Name
    +string ImageName
    +ObservableCollection~DirectoryItemViewModel~ Children
    +bool CanExpand
    +bool IsExpanded
    +ICommand ExpandCommand
    +DirectoryItemViewModel(fullPath, type)
    -void ClearChildren()
    -void Expand()
}

class DirectoryStructure{
    <<static>>
  +List~DirectoryItem~ GetLogicalDrives()$
  +List~DirectoryItem~ GetDirectoryContents(fullPath)$
  +string GetFileFolderName(path)$
}

style DirectoryStructure stroke:#FB8C00

class DirectoryItem {
    +DirectoryItemType Type
    +string FullPath
    +string Name
}

class BaseViewModel {
    +PropertyChangedEventHandler PropertyChanged
}

class RelayCommand {
    -Action mAction
    +RelayCommand(action)
    +bool CanExecute(parameter)
    +void Execute(parameter)
}

class HeaderToImageConverter {
    <<static>> +Instance
    +object Convert(value, targetType, parameter, culture)
    +object ConvertBack(value, targetType, parameter, culture)
}

class DirectoryItemType {
    <<enumeration>>
    Drive
    File
    Folder
}
style DirectoryItemType stroke:purple


%% 关联（实线）
MainWindow --> DirectoryStructureViewModel : DataContext
DirectoryStructureViewModel --> DirectoryItemViewModel : Items
DirectoryItemViewModel --> RelayCommand : ExpandCommand
DirectoryItemViewModel --|> BaseViewModel
DirectoryStructureViewModel --|> BaseViewModel
DirectoryItem --> DirectoryItemType
DirectoryItemViewModel --> DirectoryItemType

%% 依赖（虚线）
DirectoryItemViewModel ..> DirectoryStructure : uses
DirectoryItem ..> DirectoryStructure : Name helper
DirectoryStructure ..> DirectoryItem : creates/returns
HeaderToImageConverter ..> DirectoryItemViewModel : uses ImageName
