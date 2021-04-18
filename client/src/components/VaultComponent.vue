<template>
  <div class="card-container">
    <div class="card rounded">
      <div class="d-flex align-items-end">
        <div class="row w-100 justify-content-between">
          <div class="col-12 text-light">
            <div class="row justify-content-between">
              <h6 class="keep-name text-left text-dark ml-3">
                {{ vaultProp.name }}
              </h6>
              <i class="fa fa-user icon-size text-right text-light hover-icon" aria-hidden="true"></i>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'VaultComponenet',
  props: {
    vaultProp: { type: Object, required: true }
  }

}
</script>

<style>

</style>
