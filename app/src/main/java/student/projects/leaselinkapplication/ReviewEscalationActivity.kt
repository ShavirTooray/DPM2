package student.projects.leaselinkapplication

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class ReviewEscalationActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_review_escalations)

        val urgencyInput = findViewById<EditText>(R.id.addressEdit2)
        val categoryInput = findViewById<EditText>(R.id.addressEdit3)
        val statusInput = findViewById<EditText>(R.id.addressEdit4)
        val filterButton = findViewById<Button>(R.id.button21)

        filterButton.setOnClickListener {
            val urgency = urgencyInput.text.toString().trim()
            val category = categoryInput.text.toString().trim()
            val status = statusInput.text.toString().trim()

            if (urgency.isEmpty() && category.isEmpty() && status.isEmpty()) {
                Toast.makeText(this, "Please enter at least one filter", Toast.LENGTH_SHORT).show()
            } else {
                Toast.makeText(
                    this,
                    "Filter Applied:\nUrgency: $urgency\nCategory: $category\nStatus: $status",
                    Toast.LENGTH_LONG
                ).show()
            }
        }
    }
}
