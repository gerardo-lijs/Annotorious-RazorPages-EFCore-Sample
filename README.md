# Annotorious with Razor Pages and Entity Framework Core Sample
The idea of this sample project is to show how to persist Annotorious annotations to a database using Razor Pages and Entity Framework. It's really simple and both projects are amazing so hopefully this will make it easier for .NET developers to use them!

![](images/showcase.gif)

## Libraries used
* [OpenSeadragon v2.4.2](https://openseadragon.github.io/)
An open-source, web-based viewer for high-resolution zoomable images. It works amazingly well for huge images! In this Sample I only use "small" panorama images (9197x4950), but the result is the same with huge ones.
* [Annotorious v2.2.0](https://recogito.github.io/annotorious/)
A JavaScript image annotation library. Add drawing, commenting and labeling functionality to images in Web pages
* [libvips](https://github.com/libvips/libvips)
You can use command line to generate DZI image from big JPG or directly in .NET with NetVips NuGet package.

## Notes
* It's also possible to add a save button and save all annotations changes together depending what you need. For this sample I choose to save with every edit.
* Feel free to contact me here with a PR or in Twitter with any comments you might have in better ways to do improve the JavaScript part of the code which is not my strongest skill.
