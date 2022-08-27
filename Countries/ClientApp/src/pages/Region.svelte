<script lang="ts">
    import {beforeUpdate, onMount} from "svelte";
    import {fly} from "svelte/transition";

    //Services
    import RoutingService from "../lib/services/RoutingService.js";
    import Service from "../lib/services/Service";

    //Tools
    import Condition from "../lib/tools/Condition";
    import ComponentHelper from "../lib/tools/ComponentHelper";

    import Pagination from "../lib/models/Pagination";
    import Button from "../lib/Button.svelte";
    import Input from "../lib/Input.svelte";
    import Card from "../lib/Card.svelte";

    let region: Promise<any>;
    let allCountriesRegion: Promise<[]>;
    let countryRegion: Promise<[]>;
    let search = "";
    let oldSearch = "";
    let searchTimeout;
    let countriesLength = 0;

    onMount(async () => {
        const routingService = new RoutingService();

        if (Condition.isNothing(routingService.paramObject) || Condition.isNothing(routingService.paramObject.region))
            return;

        let regionName = routingService.paramObject.region as string;

        region = Service.GetCountriesByRegion(regionName);

        const result = await region;

        allCountriesRegion = result.countries;

        countriesLength = (await allCountriesRegion).length;
        countryRegion = ComponentHelper.copyFirstFive(allCountriesRegion)
    });

    beforeUpdate(() => {

        if (search === oldSearch)
            return;

        if (Condition.isStringEmpty(search)) {
            search = "";
            oldSearch = "";
            countryRegion = ComponentHelper.copyFirstFive(allCountriesRegion)
            return;
        }

        if (searchTimeout)
            clearTimeout(searchTimeout);

        searchTimeout = setTimeout(async () => {
            let values: any = await allCountriesRegion;

            search = search.toLowerCase();

            countryRegion = new Promise<[]>((resolve, reject) => {
                const newResult = values.filter((v: any) =>
                    Condition.stringContain(v.name?.common.toLowerCase(), search) ||
                    Condition.stringContain(v.region?.toLowerCase(), search) ||
                    Condition.stringContain(v.subregion?.toLowerCase(), search));
                resolve(newResult);
            });

            oldSearch = search;
        }, 10);
    });

    let start = 0;

    async function next(): Promise<void> {
        const pagination = await ComponentHelper.nextResult(new Pagination(start, allCountriesRegion));
        start = pagination.start;
        countryRegion = pagination.result;
    }

    async function previse(): Promise<void> {
        const pagination = await ComponentHelper.previseResult(new Pagination(start, allCountriesRegion));
        start = pagination.start;
        countryRegion = pagination.result;
    }
</script>

<div class="header">
    <div on:click={() => RoutingService.goto("/home")}>
        <Button>Back</Button>
    </div>
    <h2>Region</h2>
    <div on:click={() => RoutingService.goto("/home")}>
        <Button>Home</Button>
    </div>
</div>
{#await region}
    <p>Loading ...</p>
{:then reg}
    {#if reg}
        <Card style="background-color: #dadada; margin: 20px;">
            <p>
                Region: <strong>{reg.name}</strong>
            </p>
            <p>
                Population: <strong>{reg.population}</strong>
            </p>
        </Card>
    {:else}
        <p>No Results</p>
    {/if}
{/await}

<div class="header">
    {#if Condition.isStringEmpty(search) && start !== 0}
        <div on:click={previse}>
            <Button>Previse</Button>
        </div>
    {:else}
        <div></div>
    {/if}
    <Input bind:value={search} label="Search Country"></Input>
    {#if Condition.isStringEmpty(search) && (start + 10)   < countriesLength}
        <div on:click={next}>
            <Button>Next</Button>
        </div>
    {/if}
</div>
{#await countryRegion}
    <p>Loading ...</p>
{:then countries}
    {#if countries}
        <div class="container">
            <ul class="responsive-table">
                <li class="table-header">
                    <div class="col col-1">Country</div>
                    <div class="col col-2">Subregion</div>
                </li>
                {#each countries as r, index}
                    <li class="table-row" in:fly={{ x: 400, duration: 400, delay: 40 * index }}>
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
