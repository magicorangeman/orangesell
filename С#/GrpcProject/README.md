# Grpc JSON Project
## Apps for automatic sending json files from client to server

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Project was completed in Microsoft Visual Studio 2022 with .NET Core 7 and ASP.NET Core.
Working with Windows\Linux OS.
Project including a client and server apps.

## Features
- Client checking specified (in app.config file) directory path for new JSON files.
- Client sends list of discovered JSON files to the server.
- On server's console need to point the type of JSON file you need to download
(or type "all" for sending all of them).
- Client sends batched JSON values with pointed type to the server,
- Server saves downloaded data to json file in directory \root\"type"\
with name "yyyy-MM-dd.json".
- In case the server send request "all" to the client, client delete files after success uploading.

Markdown is a lightweight markup language based on the formatting conventions

## Tech

Project uses a number of open source projects to work properly:

- [,NET Core 7]
- [ASP.NET Core]
- [Google.Protobuff]
- [Grpc.Net.Client]
- [GRPC.Tools]
- [MoreLinq]
