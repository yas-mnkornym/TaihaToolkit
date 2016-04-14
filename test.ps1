Import-Module .\bin\AssemblyInfoCmdlet.dll

$env:APPVEYOR_BUILD_FOLDER = "source"

(ls $env:APPVEYOR_BUILD_FOLDER -Recurse).Where{ $_.Extension -eq ".nuspec" }.ForEach{
  $assemblyInfoPath = Join-Path (Join-Path $_.DirectoryName Properties) AssemblyInfo.cs
  $assemblyInfo = Get-AssemblyInfo $assemblyInfoPath
  $tokens = $assemblyInfo.AssemblyVersion.Split(".")
  $packageVersion = $tokens[0] + "." + $tokens[1] + "." + $tokens[2]
  $xml.package.metadata.version = $packageVersion + "-beta"
  $xml.Save($_.FullName)
}