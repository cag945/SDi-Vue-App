<template>
  <div class="container-fluid mt-4">
    <h1 class="h1">GPA Records</h1>
    <b-alert :show="loading" variant="info">Loading...</b-alert>
    <b-row>
      <b-col>
        <table class="table table-striped">
          <thead>
          <tr>
            <th>ID</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Date Time</th>
            <th>GPA</th>
            <th>&nbsp;</th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="record in records" :key="record.id">
            <td>{{ record.id }}</td>
            <td>{{ record.firstName }}</td>
            <td>{{ record.lastName }}</td>
            <td>{{ record.dateOfBirth }}</td>
            <td>{{ record.gpa }}</td>
            <td class="text-right">
              <a href="#" @click.prevent="updateGpaRecord(record)">Edit</a> -
              <a href="#" @click.prevent="deleteGpaRecord(record.id)">Delete</a>
            </td>
          </tr>
          </tbody>
        </table>
      </b-col>

      <b-col lg="3">
        <b-card :title="(model.id ? 'Edit GPA ID#' + model.id : 'New GPA Record')">
          <form @submit.prevent="createGpaRecord">
            <b-form-group label="First Name">
              <b-form-input type="text" v-model="model.firstName"></b-form-input>
            </b-form-group>
            <b-form-group label="Last Name">
              <b-form-input type="text" v-model="model.lastName"></b-form-input>
            </b-form-group>
            <b-form-group label="Date Of Birth">
              <b-form-input rows="4" v-model="model.dateOfBirth" type="datetime-local"></b-form-input>
            </b-form-group>
            <b-form-group label="GPA">
              <b-form-input rows="4" v-model="model.gpa" type="number" step=".01"></b-form-input>
            </b-form-group>
            <div>
              <b-btn type="submit" variant="success">Save Record</b-btn>
            </div>
          </form>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
  import api from '@/GpaRecordsApiService';
  import { required, email, maxValue, between } from "vuelidate/lib/validators";
  
  export default {
    validations: {
      model: {
        firstName: {
          required
        },
        lastName: {
          required
        },
        dateOfBirth: {
          required,
          maxValue: value => value > new Date().toISOString()
        },
        gpa: {
          between: between(0, 4)
        }
      }
    },
    data() {
      return {
        gpa: 0,
        loading: false,
        records: [],
        model: {}
      };
    },
    async created() {
      this.getAll()
    },
    methods: {
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
