chcp 65001

magick composite -dissolve 50%x50% map2original.png map3original.png map4original.png

set ROTATION=-10
set CROP=1600x800+300+400

magick convert map1original.png -rotate %ROTATION% -gravity southeast -crop %CROP% map1.png
magick convert map2original.png -rotate %ROTATION% -gravity southeast -crop %CROP% map2.png
magick convert map3original.png -rotate %ROTATION% -gravity southeast -crop %CROP% map3.png
magick convert map4original.png -rotate %ROTATION% -gravity southeast -crop %CROP% map4.png
