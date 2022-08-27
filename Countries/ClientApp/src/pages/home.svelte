<script lang="ts">
    import {beforeUpdate, onMount} from "svelte";
    import {fly} from "svelte/transition";

    //Services
    import RoutingService from "../lib/services/RoutingService.js";
    import Service from "../lib/services/Service";

    //Tools
    import Condition from "../lib/tools/Condition";
    import ComponentHelper from "../lib/tools/ComponentHelper";

    import Input from "../lib/Input.svelte";
    import Pagination from "../lib/models/Pagination";
    import Button from "../lib/Button.svelte";

    let allResult: Promise<[]>;
    let result: Promise<[]>;
    let search = "";
    let oldSearch = "";
    let searchTimeout;

    onMount(() => {
        allResult = Service.GetAll();
        result = ComponentHelper.copyFirstFive(allResult)
    });

    beforeUpdate(() => {

        if (search === oldSearch)
            return;

        if (Condition.isStringEmpty(search)) {
            search = "";
            oldSearch = "";
            result = ComponentHelper.copyFirstFive(allResult)
            return;
        }

        if (searchTimeout)
            clearTimeout(searchTimeout);

        searchTimeout = setTimeout(async () => {
            let values = await allResult;

            search = search.toLowerCase();

            result = new Promise<[]>((resolve, reject) => {
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
        const pagination = await ComponentHelper.nextResult(new Pagination(start, allResult));
        start = pagination.start;
        result = pagination.result;
    }

    async function previse(): Promise<void> {
        const pagination = await ComponentHelper.previseResult(new Pagination(start, allResult));
        start = pagination.start;
        result = pagination.result;
    }
</script>

<div class="header">
    {#if Condition.isStringEmpty(search) && start !== 0}
        <div on:click={previse}>
            <Button>Previse</Button>
        </div>
    {:else}
        <div></div>
    {/if}
    <Input bind:value={search} label="Search Country"></Input>
    {#if Condition.isStringEmpty(search)}
        <div on:click={next}>
            <Button>Next</Button>
        </div>
    {/if}
</div>
{#await result}
    <p>Loading ...</p>
{:then res}
    {#if Condition.isArrayNotEmpty(res)}
        <div class="container">
            <ul class="responsive-table">
                <li class="table-header">
                    <div class="col col-1">Country</div>
                    <div class="col col-2">Region</div>
                    <div class="col col-3">Subregion</div>
                </li>
                {#each res as r, index}
                    <li class="table-row" in:fly={{ x: 400, duration: 400, delay: 40 * index }}>
                        <div class="col col-1" data-label="Country"
                             on:click={() => RoutingService.goto(`/country?name=${r.name.common}`)}>
                            {r.name.common}
                        </div>
                        <div class="col col-2" data-label="Region"
                             on:click={() => RoutingService.goto(`/country/region?region=${r.region}`)}>
                            {r.region}
                        </div>
                        <div class="col col-3" data-label="Subregion"
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
      flex-basis: 30%;
    }

    .col-2 {
      flex-basis: 40%;
    }

    .col-3 {
      flex-basis: 30%;
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

