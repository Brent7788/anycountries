<script lang="ts">
    import {onMount} from "svelte";
    import RouteComponent from "./lib/models/RouteComponent";
    import Routes from "./lib/Routes.svelte";

    //Services
    import RoutingService from "./lib/services/RoutingService";

    //Pages
    import Home from "./pages/Home.svelte";
    import Error from "./pages/Error.svelte";
    import Country from "./pages/Country.svelte";
    import Region from "./pages/Region.svelte";
    import CountryByBorder from "./pages/CountryByBorder.svelte";
    import Subregion from "./pages/Subregion.svelte";


    let components: RouteComponent[] = [];
    let routingService: RoutingService;

    onMount(() => {

        routingService = new RoutingService();

        const interval = setInterval(() => {
            let path = window.location.hash;

            if (routingService.fullPath === path || (path === '' && routingService.fullPath === '#/'))
                return;

            RoutingService.previsePage = routingService.fullPath;

            routingService.fullPath = path;
        }, 50);

        //Dependency create here
        components.push(new RouteComponent(Home, ""));
        components.push(new RouteComponent(Home, "home"));

        components.push(new RouteComponent(Country, "country", ["name"]))
        components.push(new RouteComponent(CountryByBorder, "countries", ["name"]))
        components.push(new RouteComponent(Region, "country/region", ["region"]))
        components.push(new RouteComponent(Subregion, "country/subregion", ["subregion"]))
        components.push(new RouteComponent(Error, "error", ["message"]));

        return () => {
            clearInterval(interval);
        };
    });

</script>

<Routes bind:components bind:routingService/>
