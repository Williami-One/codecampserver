powershell -NoProfile -ExecutionPolicy unrestricted -Command "& {Import-Module '.\psake.psm1'; invoke-psake .\default.ps1; if ($Error -ne '') {write-host "ERROR: $error" -fore RED; exit $error.Count} }" 

