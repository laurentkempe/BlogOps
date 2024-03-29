﻿# BlogOps ![.NET](https://github.com/laurentkempe/BlogOps/workflows/.NET/badge.svg)

Tool to operate my blog at https://laurentkempe.com/.
Inspired by [Frankenblog](https://khalidabuhakmeh.com/supercharge-blogging-with-frankenblog) from [@khalidabuhakmeh](https://github.com/khalidabuhakmeh).  

## Uses

* [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)
* [Hexo](https://hexo.io/) running with [Node.js](https://nodejs.org/en/)

## Installation

> ⚠️ Use Rider Release & Install run configuration or manually

Create the NuGet package
> dotnet pack -c Release

Installing BlogOps globally
> dotnet tool install --global --add-source .\nupkg\ BlogOps

Utility to start blogging with Windows Terminal

        {
            "command": 
            {
                "action": "wt",
                "commandline": "; split-pane -p \"Powershell\" --startingDirectory \"C:\\Users\\XYZ\\\\blog\" BlogOps.exe edit ; split-pane --horizontal -p \"Powershell\" --startingDirectory \"C:\\Users\\XYZ\\blog\" BlogOps.exe server --draft",
                "startingDirectory": "C:\\Users\\XYZ\\blog\\"
            },
            "keys": "alt+shift+e",
            "name": "Blog"
        }

# Road Map

## V1
- [x] Command to create a new draft with some sample markdown as content
- [x] Command to see a list of all drafts
  - [x] See some information extracted from Markdown like title, tags
- [x] Command to publish a draft
    - [x] Adapting all dates
    - [x] Moving it from draft to posts folder
- [x] Command to start local server
  - [x] Option to display or not drafts
- [x] Deploy BlogOps to the repository of my blog

## V1.1
- [x] Command to edit a draft
- [x] Make BlogOps a dotnet tool
- [ ] Command to rename a draft

## V2
- [ ] Replace current Github Actions to create new draft with BlogOps 
- [ ] Add new Github Actions to publish a draft with BlogOps directly from Github 
- [ ] Command to run an update of hexo
- [ ] Command to run an update of theme
- [ ] Create a github repository template which can serve as a template for others

## Idea

- [ ] Command to create a new presentation with some sample markdown in it

## License

This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org/>
