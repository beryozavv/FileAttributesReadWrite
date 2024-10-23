# Windows ADS examples
>ADS path format: {filePath}:{key}
```shell
.\FileAttributesReadWrite.exe w "D:\temp\test19.docx"
.\FileAttributesReadWrite.exe r "D:\temp\test19.docx"
```
# Linux xAttr examples
>xAttr key format: user.{key} 
```shell
./FileAttributesReadWrite w "/mnt/d/temp/test19.docx"
./FileAttributesReadWrite r "/mnt/d/temp/test19.docx"
```
## How to copy files with xAttr
```shell
sudo cp --preserve=xattr /home/beryozavv/vhd-part1/test.docx /mnt/d/temp/test15.docx

sudo cp -a /home/beryozavv/vhd-part1/test.docx /home/beryozavv/vhd-part1/folder/testA.docx
```