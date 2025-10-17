package student.projects.leaselinkapplication

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity

class ConfirmationActivity: AppCompatActivity()
{

        override fun onCreate(savedInstanceState: Bundle?) {
            super.onCreate(savedInstanceState)
            setContentView(R.layout.activity_confirmation) // replace with your XML file name

            // Bind views
            val tvFullNameLabel = findViewById<TextView>(R.id.textView37)
            val tvFullNameValue = findViewById<TextView>(R.id.tvApplyNow2)

            val tvPropertyLabel = findViewById<TextView>(R.id.textView38)
            val tvPropertyValue = findViewById<TextView>(R.id.tvApplyNow3)

            val tvDateLabel = findViewById<TextView>(R.id.textView39)
            val tvDateValue = findViewById<TextView>(R.id.tvApplyNow4)

            val completeButton = findViewById<Button>(R.id.button5)

            // Get data from intent
            val fullName = intent.getStringExtra("FULL_NAME") ?: "N/A"
            val propertyName = intent.getStringExtra("PROPERTY_NAME") ?: "N/A"
            val submissionDate = intent.getStringExtra("SUBMISSION_DATE") ?: "N/A"

            // Set values in TextViews
            tvFullNameValue.text = fullName
            tvPropertyValue.text = propertyName
            tvDateValue.text = submissionDate

            // Complete button click
            completeButton.setOnClickListener {
                // Go back to main screen or any other activity
                val intent = Intent(this, MainActivity::class.java)
                intent.flags = Intent.FLAG_ACTIVITY_CLEAR_TOP or Intent.FLAG_ACTIVITY_NEW_TASK
                startActivity(intent)
                finish()
            }
        }
    }
