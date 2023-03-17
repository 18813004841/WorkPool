@echo off

pushd "前端工具\Bin\"
set fullPath=%~dp0\前端工具\Bin\ExcelToCsvConverter.exe "C:\MyExcelFile.xlsx"
start %fullPath%
popd