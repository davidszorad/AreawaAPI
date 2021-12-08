# Quickstart

To generate a nuget package run:
`> dotnet pack`

List all globally registered CLI tools:
`> dotnet tool list --global`

To install generated nuget package:
`> dotnet tool install --global --add-source <project_root_path>\bin\Debug Awa`

To execute a command you either run `> awa command` or `> dotnet run command`