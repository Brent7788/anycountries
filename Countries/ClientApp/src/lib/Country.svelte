<script lang="ts">
    import {beforeUpdate} from "svelte";

    //Services
    import Service from "../lib/services/Service";
    import RoutingService from "../lib/services/RoutingService";

    //Tools
    import Condition from "../lib/tools/Condition";
    import Card from "./Card.svelte";
    import Button from "./Button.svelte";

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

<div class="header">
    <div on:click={() => RoutingService.goto("/home")}>
        <Button>Back</Button>
    </div>
    <h2>Country</h2>
    <div on:click={() => RoutingService.goto("/home")}>
        <Button>Home</Button>
    </div>
</div>

{#await result}
    <p>Loading ...</p>
{:then res}

    {#if Condition.hasSomeValue(res) && Condition.hasSomeValue(res.region)}
        <Card style="background-color: #dadada">
            <p>
                Country Name: <strong>{res.name.common}</strong>
            </p>
            <p on:click={() => RoutingService.goto(`/country/region?region=${res.region}`)}>
                Country Region: <strong>{res.region}</strong>
            </p>
            <p>
                Capital: <strong>{res.capital}</strong>
            </p>
            <p>
                Population: <strong>{res.population}</strong>
            </p>
            <p>
                Currencies:
                {#each res.officialCurrencies as cur, i}
                    <strong>{cur} {#if (i + 1) !== res.officialCurrencies.length},{/if}</strong>
                {/each}
            </p>
            <p>
                Languages:
                {#each res.officialLanguages as lan, i}
                    <strong>{lan} {#if (i + 1) !== res.officialLanguages.length},{/if}</strong>
                {/each}
            </p>
        </Card>
        {#if Condition.hasSomeValue(res.borders)}
            <div class="container">
                <ul class="responsive-table">
                    <li class="table-header">
                        <div class="col col-1">Borders</div>
                    </li>
                    {#each res.borders as border}
                        <li class="table-row">
                            <div class="col col-1" data-label="Borders"
                                 on:click={() => RoutingService.goto(`/${nextCountryRout}?name=${border}`)}>
                                {border}
                            </div>
                        </li>
                    {/each}
                </ul>
            </div>
        {/if}

    {:else}
        <p>No Results</p>
    {/if}
{/await}

<style lang="scss">

  h2 {
    margin: 0;
  }

  p {
    margin: 5px;
  }

  .header {
    display: grid;
    grid-template-columns: 20% 60% 20%;
  }

  body {
    font-family: 'lato', sans-serif;
  }

  .container {
    max-width: 1000px;
    margin-left: auto;
    margin-right: auto;
    padding-left: 10px;
    padding-right: 10px;
  }

  .responsive-table {
    padding: 0;

    li {
      border-radius: 3px;
      padding: 5px 10px;
      display: flex;
      justify-content: space-between;
      margin-bottom: 5px;
    }

    li:hover {
      box-shadow: 0 5px 15px rgba(0, 0, 0, 0.10), 0 5px 5px rgba(0, 0, 0, 0.10);
    }

    .table-header {
      background-color: #95A5A6;
      font-size: 14px;
      text-transform: uppercase;
      letter-spacing: 0.03em;
    }

    .table-row {
      background-color: #ffffff;
      box-shadow: 0px 0px 9px 0px rgba(0, 0, 0, 0.1);
    }

    @media all and (max-width: 767px) {
      .table-header {
        display: none;
      }
      .table-row {

      }
      li {
        display: block;
      }
      li:hover {
        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.10), 0 5px 5px rgba(0, 0, 0, 0.10);
      }
      .col {

        flex-basis: 100%;

      }
      .col {
        display: flex;
        padding: 10px 0;

        &:before {
          color: #6C7A89;
          padding-right: 10px;
          content: attr(data-label);
          flex-basis: 50%;
          text-align: right;
        }
      }
    }
  }
</style>
