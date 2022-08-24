<script lang="ts">
    import {onMount} from "svelte";

    //Services
    import RoutingService from "../lib/services/RoutingService.js";
    import Service from "../lib/services/Service";
    import Condition from "../lib/tools/Condition";

    let result: Promise<[]>;

    onMount(() => {
        const routingService = new RoutingService();

        if (Condition.isNothing(routingService.paramObject) || Condition.isNothing(routingService.paramObject.region))
            return;

        let region = routingService.paramObject.region as string;

        console.log(region);
        result = Service.GetCountriesByRegion(region);
    });
</script>

{#await result}
    <p>Loading ...</p>
{:then res}
    {#if res}
        <p>Region: {res.name}</p>
        <p>Population: {res.population}</p>
        <table>
            <tr>
                <th>Country</th>
                <th>Subregion</th>
            </tr>
            {#each res.countries as r}
                <tr>
                    <td on:click={() => RoutingService.goto(`/country?name=${r.name.common}`)}>{r.name.common}</td>
                    <td on:click={() => RoutingService.goto(`/country/subregion?subregion=${r.subregion}`)}>{r.subregion}</td>
                </tr>
            {/each}
        </table>
    {:else}
        <p>No Results</p>
    {/if}
{/await}
