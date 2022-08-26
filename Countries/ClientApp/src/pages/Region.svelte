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
        <div class="container">
            <ul class="responsive-table">
                <li class="table-header">
                    <div class="col col-1">Country</div>
                    <div class="col col-2">Subregion</div>
                </li>
                {#each res.countries as r}
                    <li class="table-row">
                        <div class="col col-1" data-label="Country"
                             on:click={() => RoutingService.goto(`/country?name=${r.name.common}`)}>
                            {r.name.common}
                        </div>
                        <div class="col col-2" data-label="Subregion"
                             on:click={() => RoutingService.goto(`/country/subregion?subregion=${r.subregion}`)}>
                            {r.subregion}
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

    .col-1 {
      flex-basis: 50%;
    }

    .col-2 {
      flex-basis: 50%;
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
