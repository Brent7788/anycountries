
<a><!-- link in here -->
    <div class="button style"><slot></slot></div>
</a>

<style>

    a {
        text-decoration: none;
    }

    /* *** buttons *** */

    .button {
        width: calc(90% - 8px);
        width: -o-calc(90% - 8px);
        width: -moz-calc(90% - 8px);
        width: -webkit-calc(90% - 8px);
        width: expression(90% - 8px);
        height: auto;
        padding: 5px 0px 5px 0px;
        cursor: pointer;
        box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);

        font-family: "Helvetica";
        font-size: 12px;
        font-weight: 100;
        text-transform: uppercase;
        text-align: center;

        -o-transition: .25s;
        -ms-transition: .25s;
        -moz-transition: .25s;
        -webkit-transition: .25s;
        transition: .25s;
    }

    .button.style {
        background-color: #14967e;
        border: 2px solid #fff;
        border-radius: 50px;
        color: #fff;
    }

    a:hover .button.style {
        background-color: #fff;
        color: #14967e;
        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.10), 0 5px 5px rgba(0, 0, 0, 0.10);
    }
</style>
