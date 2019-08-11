#!/bin/sh
dotnet ef database update
dotnet BESL.Web.dll --environment=Development