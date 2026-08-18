# WinForms Modal & State Sync Guidelines for AssemblyLineTester

## 1. LoadProgramModalForm Conventions
- The file browsing button (`btnBrowseFile`) in `LoadProgramModalForm` is enabled / visible by default (`ShowBrowseButton = true`).
- When creating instances of `LoadProgramModalForm`, set `TitleText` appropriately based on intent (e.g., `"CHANGE TEST PROGRAM"` or `"LOAD TEST PROGRAM"`).
- Always ensure custom WinForms properties that affect layout at runtime include `[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]`.

## 2. Cross-Page State Synchronization
- Pages displaying active program info (such as `TestExecution` and `Dashboard`) must implement `VisibleChanged` handling (`RefreshActiveConfiguration()`).
- When a page becomes visible (`this.Visible && !DesignMode`), compare current local config against `TestConfigService.GetActiveConfig()` and refresh header, program info cards, and data grid if modified.
