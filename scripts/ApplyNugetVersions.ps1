# Working directory should be set to /source

$ProjectRoot = $args[0]

$assemblyInfoCmdletPath = Join-Path (Join-Path $ProjectRoot bin) AssemblyInfoCmdlet.dll
Import-Module $assemblyInfoCmdletPath

(ls $ProjectRoot -Recurse).Where{ $_.Extension -eq ".nuspec" }.ForEach{
	try{
		$assemblyInfoPath = Join-Path (Join-Path $_.DirectoryName Properties) AssemblyInfo.cs
		$assemblyInfo = Get-AssemblyInfo $assemblyInfoPath
		$tokens = $assemblyInfo.AssemblyVersion.Split(".")
		$packageVersion = $tokens[0] + "." + $tokens[1] + "." + $tokens[2]

		[xml]$xml = Get-Content $_.FullName
		$xml.package.metadata.version = $packageVersion + "-beta"
		$xml.Save($_.FullName)
	}
	catch [DirectoryNotFoundException]{
		Write-Host "Failed to load Assembly.cs."
		Write-Host "Skipping " + $assemblyInfoPath
	}
}