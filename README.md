# ThreadPool starvation diagnostic sample

Aspnet app sample to detect threadpool starvation using:
- [`dotnet-counters`](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-counters)
- [`dotnet-stack`](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-stack)
- [`hey`](https://github.com/rakyll/hey)
- [`dotnet-monitor`](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-monitor)
- [`prometheus`](https://prometheus.io/)
- [`grafana`](https://grafana.com/docs/grafana/latest/)

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

## Step by step - dotnet-monitor + prometheus + grafana

Running the app will start a container with `dotnet-monitor`. This container is setup to get diagnostic commands from the app container through a [diagnostic port](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/diagnostic-port). Dotnet monitor exposes endpoints with usefull information:

- http://localhost:52323/info
- http://localhost:52323/processes
- http://localhost:52325/metrics

A full list can be seem here: https://github.com/dotnet/dotnet-monitor/blob/main/documentation/api/README.md.

The metrics endpoint captures metrics in the Prometheus exposition format.

To start grafana use the command bellow:

```
docker compose up grafana
```

The compose provisions grafana with prometheus datasource and the community provided [dotnet-monitor dashboard](https://grafana.com/grafana/dashboards/19297-dotnet-monitor-dashboard/). Once is started up go to http://localhost:3000/dashboards (user: `admin`, password: `admin`)


## Load tests

Start the load test that hits the sync endpoint

```
docker compose up send-load-sync
```

Start the load test that hits the async endpoint

```
docker compose up send-load-async
```