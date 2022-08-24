<script lang="ts">

    import {onMount} from "svelte";

    //Services
    import RoutingService from "../lib/services/RoutingService";

    //Tools
    import Condition from "../lib/tools/Condition";

    import Country from "../lib/Country.svelte";
    import Service from "../lib/services/Service";

    let name: string;
    let data: string;

    onMount(() => {
        const routingService = new RoutingService();

        if (Condition.isNothing(routingService.paramObject))
            return;

        name = routingService.paramObject.name as string

/*        if (Condition.hasSomeValue(routingService.paramObject.data)) {
            data = routingService.paramObject.data as string
        } else if (Condition.hasSomeValue(routingService.paramObject.name)) {
            name = routingService.paramObject.name as string
        }*/
    });
</script>

<Country bind:data bind:name nextCountryRout="countries"></Country>

<!--<script lang="ts">

    import {onMount} from "svelte";

    //Services
    import Service from "../lib/services/Service";
    import RoutingService from "../lib/services/RoutingService";

    //Tools
    import Condition from "../lib/tools/Condition";

    let result: Promise<{}>;

    onMount(() => {
        const routingService = new RoutingService();

        if (Condition.isNothing(routingService.paramObject))
            return;

        console.log(routingService.paramObject)
        if (Condition.hasSomeValue(routingService.paramObject.data)) {
            let data = routingService.paramObject.data as string;
            data = data.replaceAll("%20", " ");
            data = data.replaceAll("%22", "\"");

            result = new Promise<{}>((resolve, rj) => resolve(JSON.parse(data)));
        } else if (Condition.hasSomeValue(routingService.paramObject.name)) {
            const name = routingService.paramObject.name as string;

            result = Service.GetCountriesByName(name);
        }
    });
</script>

{#await result}
    <p>Loading ...</p>
{:then res}

    {#if Condition.hasSomeValue(res) && Condition.hasSomeValue(res.region)}
        <p>Country Name: {res.name.common}</p>
        <p>Country Region: {res.region}</p>
        <p>Capital: {res.capital}</p>
        <p>Population: {res.population}</p>
        <p>Borders:</p>
        {#if Condition.hasSomeValue(res.borders)}
            <ul>
                {#each res.borders as border}
                    <li on:click={() => RoutingService.goto(`/countries?name=${border}`)}>{border}</li>
                {/each}
            </ul>
        {/if}
    {:else}
        <p>No Results</p>
    {/if}
{/await}-->
