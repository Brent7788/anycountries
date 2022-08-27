<script lang="ts">
    import {onMount} from "svelte";
    import {fly} from "svelte/transition";

    //Services
    import RoutingService from "../lib/services/RoutingService.js";
    import Service from "../lib/services/Service";
    import Condition from "../lib/tools/Condition";
    import Card from "../lib/Card.svelte";
    import Button from "../lib/Button.svelte";

    let result: Promise<[]>;

    onMount(() => {
        const routingService = new RoutingService();

        if (Condition.isNothing(routingService.paramObject) || Condition.isNothing(routingService.paramObject.subregion))
            return;

        let subregion = routingService.paramObject.subregion as string;
        result = Service.GetCountriesBySubregion(subregion);
    });
</script>

<div class="header">
    <div on:click={() => RoutingService.goto("/home")}>
        <Button>Back</Button>
    </div>
    <h2>Subregion</h2>
    <div on:click={() => RoutingService.goto("/home")}>
        <Button>Home</Button>
    </div>
</div>

{#await result}
    <p>Loading ...</p>
{:then res}
    {#if res}
        <Card style="background-color: #dadada">
            <p>
                Subregion: <strong>{res.subRegion}</strong>
            </p>
            <p>
                Population: <strong>{res.population}</strong>
            </p>
            <p on:click={() => RoutingService.goto(`/country/region?region=${res.region}`)}>
                Region: <strong>{res.region}</strong>
            </p>
        </Card>
        <div class="container">
            <ul class="responsive-table">
                <li class="table-header">
                    <div class="col col-1">Country</div>
                </li>
                {#each res.countries as r, index}
                    <li class="table-row" in:fly={{ x: 400, duration: 400, delay: 40 * index }}>
                        <div class="col col-1" data-label="Country"
                             on:click={() => RoutingService.goto(`/country?name=${r.name.common}`)}>
                            {r.name.common}
                        </div>
                    </li>
                {/each}
            </ul>
        </div>
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
      padding: 10px 15px;
      display: flex;
      justify-content: space-between;
      margin-bottom: 10px;
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
