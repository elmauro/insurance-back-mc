### mm/dd/yyyy ###
date +'%m%d%Y'
 
## Time in 12 hr format ###
date +'%H%M%S'
 
## backup dir format ##
backup_dir="INSURANCEBACK"$(date +'%m%d%Y%H%M%S')".zip"
echo "${backup_dir}"

dotnet build MC.Insurance/MC.Insurance.Back.sln
dotnet publish MC.Insurance/MC.Insurance.Back.sln

rm -rf packages/*.zip

cd MC.Insurance/bin/Debug/netcoreapp3.1/publish

../../../../../tools/zip a ../../../../../packages/${backup_dir} * -r