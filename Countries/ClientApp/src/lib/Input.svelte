<script lang="ts">
    /*import Icon from "svelte-awesome/components/Icon.svelte";
    import {cloudUpload} from "svelte-awesome/icons";*/
    import {onMount} from "svelte";

    export let id = "";
    export let value = "";
    export let placeholder = "";
    export let label = "None";
    export let style = "";
    export let devStyle = "";
    export let type = "";
    export let accept = "";

    onMount(() => {
        if (type === 'file' && (label === "None" || label === undefined)) {
            label = "Choose a image…";
        }
        if (id === "") {
            id = label;
        }
    });

</script>

<style lang="scss">
  .normal {
    padding: 4px 40px 8px 1rem;

    label {
      display: inline;
      color: black;
      margin: 4px 40px 4px 0px;
    }

    input {
      display: inline;
      outline: none;
      color: #050505;
      /*margin: 4px 40px 4px 40px;*/
      //Note* Never margin a input
      border-radius: 4px;
      background: #ffffff;
      box-sizing: border-box;
      box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2);
      border: 1px solid #9a6e6e;
      padding: 1px 14px;
      height: 36px;
      width: 50%;
    }
  }
</style>

<div class="normal" style={devStyle}>
    <label for={id}>{label}</label>
    {#if type === "number"}
        <input style={style} {id} {placeholder} type="number" bind:value/>
    {:else if type === "password"}
        <input style={style} {id} {placeholder} type="password" bind:value/>
    {:else}
        <input style={style} {id} {placeholder} type="text" bind:value/>
    {/if}
</div>
