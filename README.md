# ThreadPool starvation diagnostic sample

Aspnet app sample to detect threadpool starvation using:
- [`dotnet-counters`](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-counters)
- [`dotnet-stack`](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-stack)
- [`hey`](https://github.com/rakyll/hey)

## Step by step

Run the app using docker

```
docker compose up app
```

Monitor the application using dotnet-counters

```
docker exec -it thread-pool-test-app dotnet-counters monitor -n dotnet
```

Start the load test that hits the sync endpoint

```
docker compose up send-load-sync
```

Start the load test that hits the async endpoint

```
docker compose up send-load-async
```

To get the `dotnet-report` run the following

```
docker exec -it thread-pool-test-app dotnet-stack report -n dotnet
```
