<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <div class="col-md-3">
                <filter-menu></filter-menu>
            </div>
            <div class="col-sm-9">
                <reps></reps>
            </div>
        </div>
    </div>
</template>

<script>
    import Filter from './filter-menu'
    import Reps from './reps'

    export default {
      components: {
        'filter-menu': Filter,
        "reps": Reps
      },

      data () {
        return {}
      }
    }
</script>

<style>
</style>
