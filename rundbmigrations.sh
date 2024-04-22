#!/bin/bash

# Wait for database to be ready
while ! nc -z db 5432; do
  echo "Waiting for the database to be ready..."
  sleep 1
done

# Run EF Core migrations
dotnet ef database update --project ReadingIsGood
