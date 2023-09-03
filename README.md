# ThreadPool starvation diagnostic sample

Aspnet app sample to detect threadpool starvation using:
- [`dotnet-counters`](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-counters)
- [`dotnet-stack`](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-stack)
- [`hey`](https://github.com/rakyll/hey)
- [`dotnet-monitor`](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-monitor)

## Step by step - dotnet-counters

Run the app using docker

```
docker compose up app
```

Monitor the application using dotnet-counters

```
docker exec -it thread-pool-test-app dotnet-counters monitor -n dotnet
```

To get the `dotnet-stack` run the following

```
docker exec -it thread-pool-test-app dotnet-stack report -n dotnet
```

## Step by step - dotnet-monitor

```
docker compose up monitor
```

Access http://localhost:52325/metrics to see the metrics or http://localhost:52325/ to check all the endpoints. 


## Load tests

Start the load test that hits the sync endpoint

```
docker compose up send-load-sync
```

Start the load test that hits the async endpoint

```
docker compose up send-load-async
```