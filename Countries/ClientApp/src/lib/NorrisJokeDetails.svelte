<script lang="ts">
    import Card from "./Card.svelte";
    import Condition from "./tools/Condition.js";

    export let joke;

    //TODO use this
    function capitalizeFirstLetter(str) {
        return str.replace(/^./, str[0].toUpperCase());
    }
</script>

{#if Condition.hasSomeValue(joke)}
    <Card>
        <label>Category:</label>
        {#if joke.categories[0]}
            <strong>{capitalizeFirstLetter(joke.categories[0])}</strong>
        {:else}
            <strong>None</strong>
        {/if}
        <br>
        <label>Created At:</label>
        {joke.created_at.split(" ")[0]} <!--TODO Find better way-->
        <br>
        <label>Joke:</label>
        <strong>{joke.value}</strong>
    </Card>
{/if}