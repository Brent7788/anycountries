<script lang="ts">

    import {onMount} from "svelte";

    //Services
    import RoutingService from "../lib/services/RoutingService";

    //Tools
    import Condition from "../lib/tools/Condition";

    import Country from "../lib/Country.svelte";

    let name: string;
    let data: string;

    onMount(() => {
        const routingService = new RoutingService();

        if (Condition.isNothing(routingService.paramObject))
            return;

        name = routingService.paramObject.name as string
    });
</script>

<Country bind:data bind:name nextCountryRout="countries"></Country>
