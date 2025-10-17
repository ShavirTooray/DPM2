package student.projects.leaselinkapplication

import android.app.Activity
import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.widget.*
import androidx.appcompat.app.AppCompatActivity

class MaintenanceRequestActivity : AppCompatActivity() {

    private var selectedFileUri: Uri? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_maintenance_request)

        val nameInput = findViewById<EditText>(R.id.editTextText3)
        val surnameInput = findViewById<EditText>(R.id.editTextText4)
        val unitInput = findViewById<EditText>(R.id.editTextText8)
        val emailInput = findViewById<EditText>(R.id.editTextTextEmailAddress)
        val areaCodeInput = findViewById<EditText>(R.id.editTextNumber)
        val phoneInput = findViewById<EditText>(R.id.editTextPhone)
        val detailsInput = findViewById<EditText>(R.id.editTextText5)
        val uploadFileButton = findViewById<Button>(R.id.button9)
        val submitButton = findViewById<Button>(R.id.button10)

        uploadFileButton.setOnClickListener {
            val intent = Intent(Intent.ACTION_GET_CONTENT)
            intent.type = "*/*"
            startActivityForResult(intent, 101)
        }

        submitButton.setOnClickListener {
            val name = nameInput.text.toString().trim()
            val surname = nameInput.text.toString().trim()
            val unit = unitInput.text.toString().trim()
            val email = emailInput.text.toString().trim()
            val areaCode = areaCodeInput.text.toString().trim()
            val phone = phoneInput.text.toString().trim()
            val details = detailsInput.text.toString().trim()

            if (name.isEmpty() || unit.isEmpty() || email.isEmpty() ||
                areaCode.isEmpty() || phone.isEmpty() || details.isEmpty()) {
                Toast.makeText(this, "Please fill in all fields", Toast.LENGTH_SHORT).show()
            } else {
                val fullPhone = "$areaCode-$phone"
                val fileName = selectedFileUri?.lastPathSegment ?: "No file was attached"

                // Simulate sending to manager (e.g., log or Toast)
                val summary = """
                    Request Submitted:
                    Name: $name
                    Unit: $unit
                    Email: $email
                    Phone: $fullPhone
                    Details: $details
                    File: $fileName
                """.trimIndent()

                Toast.makeText(this, "Request sent to manager!", Toast.LENGTH_LONG).show()
                println(summary) // You can replace this with actual sending logic later

                finish() // Optionally close the form
            }
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (requestCode == 101 && resultCode == Activity.RESULT_OK) {
            selectedFileUri = data?.data
            val fileName = selectedFileUri?.lastPathSegment ?: "Unknown file"
            Toast.makeText(this, "File selected: $fileName", Toast.LENGTH_SHORT).show()
        }
    }
}
