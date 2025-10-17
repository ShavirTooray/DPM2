package student.projects.leaselinkapplication

import android.app.Activity
import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.snackbar.Snackbar

class ApplicationActivity : AppCompatActivity() {

        private lateinit var uploadButton: Button
        private lateinit var applyButton: Button
        private var selectedFileUri: Uri? = null

        companion object {
            private const val FILE_PICKER_REQUEST = 1001
        }

        override fun onCreate(savedInstanceState: Bundle?) {
            super.onCreate(savedInstanceState)
            setContentView(R.layout.activity_application)


            // Make sure this is inside onCreate after setContentView
            val fullNameEdit = findViewById<EditText>(R.id.FullName)
            val idNumberEdit = findViewById<EditText>(R.id.idNumber)
            val emailEdit = findViewById<EditText>(R.id.emailEdit)
            val phoneEdit = findViewById<EditText>(R.id.phoneEdit)
            val addressEdit = findViewById<EditText>(R.id.addressEdit)
            val postalCodeEdit = findViewById<EditText>(R.id.postalCodeEdit)
            val radioGroup = findViewById<RadioGroup>(R.id.radioGroup)
            val jobTitleEdit = findViewById<EditText>(R.id.jobTitleEdit)
            val employerEdit = findViewById<EditText>(R.id.employerEdit)
            val contactEdit = findViewById<EditText>(R.id.contactEdit)
            val propertyNameEdit = findViewById<EditText>(R.id.propertyNameEdit)
            val uploadEdit = findViewById<EditText>(R.id.etUpload)
            val uploadButton = findViewById<Button>(R.id.uploadButton)
            val applyButton = findViewById<Button>(R.id.btnApply)


            // Handle file upload
            uploadButton.setOnClickListener {
                val intent = Intent(Intent.ACTION_GET_CONTENT)
                intent.type = "*/*"
                startActivityForResult(Intent.createChooser(intent, "Select a document"), FILE_PICKER_REQUEST)
            }

            // Handle Apply button
            applyButton.setOnClickListener {
                // Get values
                val fullName = findViewById<EditText>(R.id.tvApplyNow).text.toString() // your title TextView is not EditText, so replace with your real FullName EditText id
                val idNumber = idNumberEdit.text.toString()
                val email = emailEdit.text.toString()
                val phone = phoneEdit.text.toString()
                val address = addressEdit.text.toString()
                val postalCode = postalCodeEdit.text.toString()
                val jobTitle = jobTitleEdit.text.toString()
                val employer = employerEdit.text.toString()
                val contact = contactEdit.text.toString()
                val propertyName = propertyNameEdit.text.toString()

                val selectedRadioId = radioGroup.checkedRadioButtonId
                val propertyType = if (selectedRadioId != -1) {
                    findViewById<RadioButton>(selectedRadioId).text.toString()
                } else ""

                // Simple validation
                if (fullName.isEmpty() || idNumber.isEmpty() || email.isEmpty() ||
                    phone.isEmpty() || address.isEmpty() || propertyName.isEmpty()) {
                    Toast.makeText(this, "Please fill in all required fields.", Toast.LENGTH_SHORT).show()
                    return@setOnClickListener
                }

                // Show confirmation
                Snackbar.make(applyButton, "Application submitted successfully!", Snackbar.LENGTH_LONG).show()
            }
        }

        override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
            super.onActivityResult(requestCode, resultCode, data)
            if (requestCode == FILE_PICKER_REQUEST && resultCode == Activity.RESULT_OK) {
                selectedFileUri = data?.data
                val uploadEdit = findViewById<EditText>(R.id.etUpload)
                uploadEdit.setText(selectedFileUri?.lastPathSegment ?: "File selected")
            }
        }
    }
