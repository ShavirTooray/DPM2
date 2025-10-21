package student.projects.leaselinkapplication

import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.button.MaterialButton

class DashboardActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_dashboard)

        val submitMaintenanceButton = findViewById<MaterialButton>(R.id.buttonSubmit)
        val makePaymentButton = findViewById<MaterialButton>(R.id.buttonPayment)
        val chatBotButton = findViewById<MaterialButton>(R.id.buttonChatbot)
        val uploadProofButton = findViewById<MaterialButton>(R.id.buttonUpload)

        submitMaintenanceButton.setOnClickListener {
            startActivity(Intent(this, MaintenanceRequestActivity::class.java))
        }

        makePaymentButton.setOnClickListener {
            startActivity(Intent(this, PaymentsActivity::class.java))
        }

        chatBotButton.setOnClickListener {
            startActivity(Intent(this, ChatBotActivity::class.java))
        }

        // If you add an UploadProofActivity later, you can enable this:
        // uploadProofButton.setOnClickListener {
        //     startActivity(Intent(this, UploadProofActivity::class.java))
        // }
    }
}
