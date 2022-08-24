<script lang="ts">
    import {onMount} from "svelte";

    //Services
    import RoutingService from "../lib/services/RoutingService.js";
    import Service from "../lib/services/Service";
    import Condition from "../lib/tools/Condition";

    let result: Promise<[]>;

    onMount(() => {
        const routingService = new RoutingService();

        if (Condition.isNothing(routingService.paramObject) || Condition.isNothing(routingService.paramObject.subregion))
            return;

        let subregion = routingService.paramObject.subregion as string;
        console.log(subregion);
        result = Service.GetCountriesBySubregion(subregion);
    });
</script>

{#await result}
    <p>Loading ...</p>
{:then res}
    {#if res}
        <p>Subregion: {res.subRegion}</p>
        <p>Population: {res.population}</p>
        <p on:click={() => RoutingService.goto(`/country/region?region=${res.region}`)}>Region: {res.region}</p>
        <table>
            <tr>
                <th>Country</th>
            </tr>
            {#each res.countries as r}
                <tr>
                    <td on:click={() => RoutingService.goto(`/country?name=${r.name.common}`)}>{r.name.common}</td>
                </tr>
            {/each}
        </table>
    {:else}
        <p>No Results</p>
    {/if}
{/await}
