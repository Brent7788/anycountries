<script lang="ts">
    import Card from "./Card.svelte";
    import Condition from "./tools/Condition.js";

    export let people;
</script>

{#if Condition.hasSomeValue(people)}
    <Card>
        <label>Name:</label>
        <strong>{people.name}</strong>
        <br>
        <label>Height:</label>
        <strong>{people.height}</strong>
        <br>
        <label>Mass:</label>
        <strong>{people.mass}</strong>
        <br>
        <label>Hair Color:</label>
        <strong>{people.hair_color}</strong>
        <br>
        <label>Skin Color:</label>
        <strong>{people.skin_color}</strong>
        <br>
        <label>Eye Color:</label>
        <strong>{people.eye_color}</strong>
        <br>
        <label>Birth Year:</label>
        <strong>{people.birth_year}</strong>
        <br>
        <label>Gender:</label>
        <strong>{people.gender}</strong>
    </Card>
{/if}