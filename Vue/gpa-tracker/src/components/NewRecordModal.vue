<template>
  <div>
    <b-button @click="modalShow = !modalShow">
      Create Record
    </b-button>
    <b-modal v-model="modalShow" @ok="handleOk">
        <form @submit.stop.prevent="handleSubmit">
            <b-form-input type="text"
                placeholder="First Name"
                v-model=model.firstName></b-form-input>
            <b-form-input type="text"
                placeholder="Last Name"
                v-model=model.lastName></b-form-input>
            <b-form-input type="date"
                placeholder="Date Of Birth"
                v-model=model.dateOfBirth></b-form-input>
            <b-form-input type="date"
                placeholder=""
                v-model=model.dateOfBirth></b-form-input>
        </form>
    </b-modal>
  </div>
</template>

<script>

import api from '@/GpaRecordsApiService';
    export default {
        props: {
            model: {}
        },
        data () {
            return {
            modalShow: false
            }
        },
        async created() {
        this.getAll()
        },
        methods: {
            handleOk (evt) {
                // Prevent modal from closing
                evt.preventDefault()
                if (!this.name) {
                    alert('Please fill out the record')
                } else {
                    this.handleSubmit()
                }
            },
            handleSubmit () {
                this.names.push(this.name)
                this.clearName()
                this.$refs.modal.hide()
            },
            async getAll() {
                this.loading = true

                try {
                this.records = await api.getAll()
                } finally {
                this.loading = false
                }
            },
            async updateGpaRecord(gpaRecord) {
                // We use Object.assign() to create a new (separate) instance
                this.model = Object.assign({}, gpaRecord)
            },
            async createGpaRecord() {
                const isUpdate = !!this.model.id;

                if (isUpdate) {
                await api.update(this.model.id, this.model)
                } else {
                await api.create(this.model)
                }

                // Clear the data inside of the form
                this.model = {}

                // Fetch all records again to have latest data
                await this.getAll()
            },
            async deleteGpaRecord(id) {
                if (confirm('Are you sure you want to delete this record?')) {
                // if we are editing a gpa record we deleted, remove it from the form
                if (this.model.id === id) {
                    this.model = {}
                }

                await api.delete(id)
                await this.getAll()
                }
            }
        }
    }
</script>