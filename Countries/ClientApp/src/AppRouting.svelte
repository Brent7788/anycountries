<script lang="ts">
    import {onMount} from "svelte";
    import RouteComponent from "./lib/models/RouteComponent";
    import Routes from "./lib/Routes.svelte";

    //Services
    import RoutingService from "./lib/services/RoutingService";

    //Pages
    import Home from "./pages/Home.svelte";
    /*import ChuckNorrisJoke from "./pages/ChuckNorrisJoke.svelte";
    import StartWarsPeople from "./pages/StartWarsPeople.svelte";
    import SearchNorrisInSpace from "./pages/search/SearchNorrisInSpace.svelte";*/
    import Error from "./pages/Error.svelte";


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

        /*components.push(new RouteComponent(ChuckNorrisJoke, "jokes/random", ["category"]));
        components.push(new RouteComponent(StartWarsPeople, "start/wars/people"));
        components.push(new RouteComponent(SearchNorrisInSpace, "space/norris/search", ["query"]));*/
        components.push(new RouteComponent(Error, "error", ["message"]));

        return () => {
            clearInterval(interval);
        };
    });

</script>

<Routes bind:components bind:routingService/>
