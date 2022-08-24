<script lang="ts">

    import {beforeUpdate, onMount} from "svelte";

    //Services
    import Service from "../lib/services/Service";
    import RoutingService from "../lib/services/RoutingService";

    //Tools
    import Condition from "../lib/tools/Condition";

    export let data: string;
    export let name: string;
    export let nextCountryRout = "countries"
    let result: Promise<{}>;

    beforeUpdate(() => {
        console.log(data, name);
        if (Condition.isNothing(data) && Condition.isNothing(name))
            return;

        if (Condition.hasSomeValue(data)) {
            data = data.replaceAll("%20", " ");
            data = data.replaceAll("%22", "\"");

            result = new Promise<{}>((resolve, rj) => resolve(JSON.parse(data)));
        } else if (Condition.hasSomeValue(name)) {
            result = Service.GetCountriesByName(name);
        }
    });
</script>

{#await result}
    <p>Loading ...</p>
{:then res}

    {#if Condition.hasSomeValue(res) && Condition.hasSomeValue(res.region)}
        <p>Country Name: {res.name.common}</p>
        <p on:click={() => RoutingService.goto(`/country/region?region=${res.region}`)}>Country Region: {res.region}</p>
        <p>Capital: {res.capital}</p>
        <p>Population: {res.population}</p>
        <p>Currencies: {res.currencies}</p>
        <p>Languages: {res.languages}</p>
        <p>Borders:</p>
        {#if Condition.hasSomeValue(res.borders)}
            <ul>
                {#each res.borders as border}
                    <li on:click={() => RoutingService.goto(`/${nextCountryRout}?name=${border}`)}>{border}</li>
                {/each}
            </ul>
        {/if}
    {:else}
        <p>No Results</p>
    {/if}
{/await}
