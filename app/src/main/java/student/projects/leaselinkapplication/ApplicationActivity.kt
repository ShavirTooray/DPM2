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

        // Inputs (match IDs from XML)
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
        val uploadPathEdit = findViewById<EditText>(R.id.etUpload)

        // Buttons (use class properties, not shadowing locals)
        uploadButton = findViewById(R.id.uploadButton)
        applyButton = findViewById(R.id.btnApply)

        // Handle file upload
        uploadButton.setOnClickListener {
            val intent = Intent(Intent.ACTION_GET_CONTENT).apply { type = "*/*" }
            startActivityForResult(Intent.createChooser(intent, "Select a document"), FILE_PICKER_REQUEST)
        }

        // Handle Apply button
        applyButton.setOnClickListener {
            // Read values from actual input fields
            val fullName = fullNameEdit.text.toString().trim()
            val idNumber = idNumberEdit.text.toString().trim()
            val email = emailEdit.text.toString().trim()
            val phone = phoneEdit.text.toString().trim()
            val address = addressEdit.text.toString().trim()
            val postalCode = postalCodeEdit.text.toString().trim()
            val jobTitle = jobTitleEdit.text.toString().trim()
            val employer = employerEdit.text.toString().trim()
            val contact = contactEdit.text.toString().trim()
            val propertyName = propertyNameEdit.text.toString().trim()

            // RadioGroup children in XML don't have IDs — find the checked one by scanning
            val propertyType = (0 until radioGroup.childCount)
                .mapNotNull { radioGroup.getChildAt(it) as? RadioButton }
                .firstOrNull { it.isChecked }
                ?.text
                ?.toString()
                ?: ""

            // Simple validation (required fields)
            if (fullName.isEmpty() || idNumber.isEmpty() || email.isEmpty() ||
                phone.isEmpty() || address.isEmpty() || propertyName.isEmpty()
            ) {
                Toast.makeText(this, "Please fill in all required fields.", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            Snackbar.make(applyButton, "Application submitted successfully!", Snackbar.LENGTH_LONG).show()
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if (requestCode == FILE_PICKER_REQUEST && resultCode == Activity.RESULT_OK) {
            selectedFileUri = data?.data
            val uploadPathEdit = findViewById<EditText>(R.id.etUpload)
            uploadPathEdit.setText(selectedFileUri?.lastPathSegment ?: "File selected")
        }
    }
}
